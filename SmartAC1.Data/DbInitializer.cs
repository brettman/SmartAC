using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace SmartAC1.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(SmartAc1DbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            context.Database.EnsureCreated();
            //await CreateTemplateTexts(context);

            if (EnumerableExtensions.Any(context.Users))
                return; // DB has been seeded

            await CreateDefaultUserAndRoleForApplication(userManager, roleManager, logger);
        }

        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<IdentityUser> um, RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger)
        {
            const string administratorRole = "Admin";
            const string email = "admintest@theorem.com";

            await CreateDefaultAdministratorRole(rm, logger, "admin");

            var user = await CreateDefaultUser(um, logger, email);
            await SetPasswordForDefaultUser(um, logger, email, user);
            await AddDefaultRoleToDefaultUser(um, logger, email, administratorRole, user);
        }

        private static async Task CreateDefaultAdministratorRole(RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger, string roleToCreate)
        {
            logger.LogInformation($"Create the role `{roleToCreate}` for application");
            var ir = await rm.CreateAsync(new IdentityRole(roleToCreate));
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the role `{roleToCreate}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{roleToCreate}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task<IdentityUser> CreateDefaultUser(UserManager<IdentityUser> um, ILogger logger, string email)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");
            //var user = new IdentityUser(email, "First", "Last", new DateTime(1970, 1, 1));
            var user = new IdentityUser
            {
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
            };


            var ir = await um.CreateAsync(user);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default user `{email}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }

            var createdUser = await um.FindByEmailAsync(email);
            return createdUser;
        }

        private static async Task SetPasswordForDefaultUser(UserManager<IdentityUser> um, ILogger<DbInitializer> logger, string email, IdentityUser user)
        {
            logger.LogInformation($"Set password for default user `{email}`");
            const string password = "Pass_word0";
            var ir = await um.AddPasswordAsync(user, password);
            if (ir.Succeeded)
            {
                logger.LogTrace($"Set password `{password}` for default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user `{email}` cannot be set");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<IdentityUser> um, ILogger<DbInitializer> logger, string email, string administratorRole, IdentityUser user)
        {
            logger.LogInformation($"Add default user `{email}` to role '{administratorRole}'");
            var ir = await um.AddToRoleAsync(user, administratorRole);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{administratorRole}' to default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{administratorRole}` cannot be set for the user `{email}`");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}