# List of entities to generate controllers for (complete list from AppDbContext)
$entities = @(
    "Consultation", "Course", "Enrollment", "Language", 
    "Level", "Material", "MaterialDistribution", "PlacementTest", "Schedule", 
    "Session", "Student", "Subs", "Teacher", "TeacherCertificate", 
    "StudentPlacementTest", "TeacherLanguage"
)

foreach ($e in $entities) {
    Write-Host "Scaffolding $e Controller..."

    dotnet aspnet-codegenerator controller `
        -name "${e}Controller" `
        -m $e `
        -actions `
        -dc AppDbContext `
        -outDir Controllers `
        --useDefaultLayout `
        --useAsyncActions `
        --referenceScriptLibraries `
        --force
}

Write-Host "All controllers scaffolded successfully!"
