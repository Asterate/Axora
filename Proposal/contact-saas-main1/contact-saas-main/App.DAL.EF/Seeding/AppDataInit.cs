using App.Domain;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Seeding;

public static class AppDataInit
{
    public static void SeedAppData(AppDbContext context)
    {
        // Seed ProjectTypes
        if (!context.ProjectTypes.Any())
        {
            context.ProjectTypes.AddRange(
                new ProjectType { Id = Guid.NewGuid(), Name = new LangStr("Research", "Uurimis") },
                new ProjectType { Id = Guid.NewGuid(), Name = new LangStr("Commercial", "Kommertsiaal") },
                new ProjectType { Id = Guid.NewGuid(), Name = new LangStr("Academic", "Akadeemiline") }
            );
        }

        // Seed ExperimentTypes
        if (!context.ExperimentTypes.Any())
        {
            context.ExperimentTypes.AddRange(
                new ExperimentType { Id = Guid.NewGuid(), ExperimentTypeName = new LangStr("Chemistry", "Keemia") },
                new ExperimentType { Id = Guid.NewGuid(), ExperimentTypeName = new LangStr("Biology", "Bioloogia") },
                new ExperimentType { Id = Guid.NewGuid(), ExperimentTypeName = new LangStr("Physics", "Füüsika") }
            );
        }

        // Seed LabTypes
        if (!context.LabTypes.Any())
        {
            context.LabTypes.AddRange(
                new LabType { Id = Guid.NewGuid(), Name = new LangStr("Chemistry Lab", "Keemia labor") },
                new LabType { Id = Guid.NewGuid(), Name = new LangStr("Biology Lab", "Bioloogia labor") },
                new LabType { Id = Guid.NewGuid(), Name = new LangStr("Physics Lab", "Füüsika labor") }
            );
        }

        // Seed EquipmentTypes
        if (!context.EquipmentTypes.Any())
        {
            context.EquipmentTypes.AddRange(
                new EquipmentType { Id = Guid.NewGuid(), EquipmentTypeName = new LangStr("Measurement", "Mõõtmine") },
                new EquipmentType { Id = Guid.NewGuid(), EquipmentTypeName = new LangStr("Analysis", "Analüüs") },
                new EquipmentType { Id = Guid.NewGuid(), EquipmentTypeName = new LangStr("Production", "Tootmine") }
            );
        }

        // Seed TaskTypes
        if (!context.TaskTypes.Any())
        {
            context.TaskTypes.AddRange(
                new TaskType { Id = Guid.NewGuid(), TaskTypeName = new LangStr("Preparation", "Ettevalmistus") },
                new TaskType { Id = Guid.NewGuid(), TaskTypeName = new LangStr("Execution", "Teostamine") },
                new TaskType { Id = Guid.NewGuid(), TaskTypeName = new LangStr("Analysis", "Analüüs") }
            );
        }

        // Seed InstituteTypes
        if (!context.InstituteTypes.Any())
        {
            context.InstituteTypes.AddRange(
                new InstituteType { Id = Guid.NewGuid(), Name = new LangStr("University", "Ülikool") },
                new InstituteType { Id = Guid.NewGuid(), Name = new LangStr("Research Institute", "Uurimisasutus") },
                new InstituteType { Id = Guid.NewGuid(), Name = new LangStr("Company", "Ettevõte") }
            );
        }

        // Seed DocumentTypes
        if (!context.DocumentTypes.Any())
        {
            context.DocumentTypes.AddRange(
                new DocumentType { Id = Guid.NewGuid(), Name = new LangStr("Report", "Raport") },
                new DocumentType { Id = Guid.NewGuid(), Name = new LangStr("Manual", "Käsiraamat") },
                new DocumentType { Id = Guid.NewGuid(), Name = new LangStr("Certificate", "Sertifikaat") }
            );
        }

        // Seed ReagentTypes
        if (!context.ReagentTypes.Any())
        {
            context.ReagentTypes.AddRange(
                new ReagentType { Id = Guid.NewGuid(), ReagentName = new LangStr("Chemical", "Keemiline") },
                new ReagentType { Id = Guid.NewGuid(), ReagentName = new LangStr("Biological", "Bioloogiline") },
                new ReagentType { Id = Guid.NewGuid(), ReagentName = new LangStr("Solution", "Lahus") }
            );
        }

        // Seed CertificationTypes
        if (!context.CertificationTypes.Any())
        {
            context.CertificationTypes.AddRange(
                new CertificationType { Id = Guid.NewGuid(), Name = "ISO 9001" },
                new CertificationType { Id = Guid.NewGuid(), Name = "ISO 14001" },
                new CertificationType { Id = Guid.NewGuid(), Name = "Safety" }
            );
        }

        // Seed Institutes
        if (!context.Institutes.Any())
        {
            var instituteTypeId = context.InstituteTypes.First().Id;
            context.Institutes.Add(new Institute
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                InstituteName = new LangStr("Default Institute", "Vaikeuasutus"),
                InstituteCountry = "Estonia",
                InstituteAddress = new LangStr("Tartu, Estonia", "Tartu, Eesti"),
                InstitutePhoneNumber = "+372 1234567",
                InstituteTypeId = instituteTypeId
            });
        }

        // Note: Test user seeding is handled separately via identity - for reference only
        // Use Register endpoint to create users with passwords

        // Seed Labs
        if (!context.Labs.Any())
        {
            var labTypeId = context.LabTypes.First().Id;
            context.Labs.Add(new Lab
            {
                Id = Guid.NewGuid(),
                LabName = "Main Lab",
                LabAddress = "Building A, Room 101",
                LabCapacity = 10,
                LabTypeId = labTypeId
            });
        }

        // Seed InstituteUsers (for FK constraints) - always add if not exists
        var instituteId = context.Institutes.First().Id;
        var appUser = context.Users.FirstOrDefault();
        if (appUser != null && !context.InstituteUsers.Any(u => u.User.Id == appUser.Id))
        {
            context.InstituteUsers.Add(new InstituteUser
            {
                Id = Guid.NewGuid(),
                InstituteId = instituteId,
                User = appUser
            });
        }
        // Add the test user as InstituteUser if not already exists
        var testUser = context.Users.FirstOrDefault(u => u.Id == InitialData.TestUserId);
        if (testUser != null && !context.InstituteUsers.Any(u => u.User.Id == testUser.Id))
        {
            context.InstituteUsers.Add(new InstituteUser
            {
                Id = Guid.NewGuid(),
                InstituteId = instituteId,
                User = testUser
            });
        }

        // Seed Projects
        if (!context.Projects.Any())
        {
            var projectTypes = context.ProjectTypes.ToList();
            if (projectTypes.Any())
            {
                // Add multiple sample projects
                context.Projects.AddRange(
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        ProjectName = "Climate Change Research",
                        ProjectTypeId = projectTypes.First().Id,
                        Funding = 50000.00f,
                        Requirements = "Research on environmental impact and sustainability solutions."
                    },
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        ProjectName = "AI in Healthcare",
                        ProjectTypeId = projectTypes.First().Id,
                        Funding = 75000.00f,
                        Requirements = "Developing machine learning models for medical diagnosis."
                    },
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        ProjectName = "Renewable Energy Study",
                        ProjectTypeId = projectTypes.First().Id,
                        Funding = 60000.00f,
                        Requirements = "Investigating solar and wind energy efficiency."
                    },
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        ProjectName = "Molecular Biology Project",
                        ProjectTypeId = projectTypes.Last().Id,
                        Funding = 80000.00f,
                        Requirements = "Study of cellular processes and genetic engineering."
                    },
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        ProjectName = "Quantum Computing Initiative",
                        ProjectTypeId = projectTypes.Skip(1).First().Id,
                        Funding = 100000.00f,
                        Requirements = "Research on quantum algorithms and computation."
                    }
                );
            }
        }

        // Seed Experiments
        if (!context.Experiments.Any())
        {
            var projectId = context.Projects.First().Id;
            var experimentTypeId = context.ExperimentTypes.First().Id;
            var instituteUserId = context.InstituteUsers.First().Id;
            context.Experiments.Add(new Experiment
            {
                Id = Guid.NewGuid(),
                ExperimentName = "Sample Experiment",
                ExperimentNotes = "Initial test experiment",
                ProjectId = projectId,
                ExperimentTypeId = experimentTypeId,
                InstituteUserId = instituteUserId
            });
        }

        context.SaveChanges();
    }

    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DeleteDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        foreach (var (roleName, id) in InitialData.Roles)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;

            if (role != null) continue;

            role = new AppRole()
            {
                Name = roleName,
            };

            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Role creation failed!");
            }
        }


        foreach (var userInfo in InitialData.Users)
        {
            var user = userManager.FindByEmailAsync(userInfo.name).Result;
            if (user == null)
            {
                user = new AppUser()
                {
                    Id = userInfo.id ?? Guid.NewGuid(),
                    Email = userInfo.name,
                    UserName = userInfo.name,
                    EmailConfirmed = true,
                    FirstName = userInfo.firstName,
                    LastName = userInfo.lastName
                };
                var result = userManager.CreateAsync(user, userInfo.password).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }
            }
            else
            {
                // Update existing user with first and last name if not already set
                if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                {
                    user.FirstName = userInfo.firstName;
                    user.LastName = userInfo.lastName;
                    userManager.UpdateAsync(user).Wait();
                }
            }

            foreach (var role in userInfo.roles)
            {
                if (userManager.IsInRoleAsync(user, role).Result)
                {
                    Console.WriteLine($"User {user.UserName} already in role {role}");
                    continue;
                }

                var roleResult = userManager.AddToRoleAsync(user, role).Result;
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                else
                {
                    Console.WriteLine($"User {user.UserName} added to role {role}");
                }
            }
        }
    }
}