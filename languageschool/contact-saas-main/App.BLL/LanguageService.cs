using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class LanguageService
{
    public static async Task<List<SelectListItem>> GetCompanyLanguagesAsync(AppDbContext context)
    {
        return await context.Languages
            .Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.LanguageName })
            .ToListAsync();
    }public static async Task<List<SelectListItem>> GetCompanyLevelsAsync(AppDbContext context)
    {
        return await context.Levels
            .Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.LevelName })
            .ToListAsync();
    }

    public static async Task<List<SelectListItem>> CompanyLanguages(AppDbContext context, Guid id)
    {
        var companyLanguages = await context.Languages
            .Where(l => l.CompanyId == id)
            .Include(l => l.Level)
            .ToListAsync();
        return companyLanguages
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LanguageName
            })
            .ToList();
    }
    
    public static async Task<Dictionary<Guid, List<SelectListItem>>> GetLanguageLevelMap(AppDbContext context, Guid companyId)
    {
        var languages = await context.Languages
            .Where(l => l.CompanyId == companyId)
            .Include(l => l.Level)
            .ToListAsync();
            
        var languageLevelMap = new Dictionary<Guid, List<SelectListItem>>();
        
        // Get all levels that are associated with any language in the company
        var allLevels = await context.Languages
            .Where(l => l.CompanyId == companyId)
            .Include(l => l.Level)
            .Select(l => l.Level!)
            .GroupBy(l => l.Id)
            .Select(g => g.First())
            .ToListAsync();

            
        // Create a list of SelectListItems for all levels
        var levelSelectList = allLevels.Select(l => new SelectListItem
        {
            Value = l?.Id.ToString(),
            Text = l?.LevelName
        }).ToList();
        
        // Map each language to all available levels
        foreach (var language in languages)
        {
            languageLevelMap[language.Id] = levelSelectList;
        }
        
        return languageLevelMap;
    }
    
    public static async Task<List<SelectListItem>> Levels(AppDbContext context)
    {
        var levels = await context.Levels.ToListAsync();
        return levels
            .Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LevelName
            })
            .ToList();
    }
    
    public static async Task<List<SelectListItem>> GetLevelForLanguage(AppDbContext context, Guid languageId)
    {
        var language = await context.Languages
            .Include(l => l.Level)
            .FirstOrDefaultAsync(l => l.Id == languageId);
            
        if (language == null || language.Level == null)
        {
            return new List<SelectListItem>();
        }
        
        return new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = language.Level.Id.ToString(),
                Text = language.Level.LevelName
            }
        };
    }
}