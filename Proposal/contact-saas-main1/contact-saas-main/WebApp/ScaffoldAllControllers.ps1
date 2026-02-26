$entities = @(
    "InstituteUser","Institute","InstituteLab","InstituteProject","InstituteType",
    "Experiment","ExperimentEquipment","ExperimentTask","Project","Result",
    "Document","DocumentResult","Schedule","Equipment","EquipmentType",
    "EquipmentCertificationType","Certification","CertificationType","Reagent","ReagentType",
    "ReagentLab","ExperimentType","TaskType","DocumentType","ProjectType","EquipmentLab",
    "Lab","LabType"
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
