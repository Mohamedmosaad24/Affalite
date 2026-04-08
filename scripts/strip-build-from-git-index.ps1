# Run after a merge if bin/obj show up again (same as .githooks/post-merge).
# Then: git commit -m "chore: untrack bin/obj after merge"
Set-Location $PSScriptRoot\..
$paths = @(
    'AffaliteBL/bin', 'AffaliteBL/obj',
    'AffaliteDAL/bin', 'AffaliteDAL/obj',
    'AffalitePL/bin', 'AffalitePL/obj'
)
git rm -r --cached --ignore-unmatch @paths
if (-not (git diff --cached --quiet 2>$null)) {
    Write-Host "`nCommit when ready: git commit -m `"chore: untrack bin/obj after merge`"`n"
}
