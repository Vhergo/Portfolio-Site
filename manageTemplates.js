// element placeholders and script directory paths
const player1 = document.getElementById('playerScript');
const player2 = document.getElementById('playerMovementScript');
const player3 = document.getElementById('playerCombatScript');
const player4 = document.getElementById('playerAnimationScript');

const enemy1 = document.getElementById('boomkinScript');
const enemy2 = document.getElementById('juggernautScript');
const enemy3 = document.getElementById('sentinelScript');

const other1 = document.getElementById('weaponSO');
const other2 = document.getElementById('generateEnemies');
const other3 = document.getElementById('spawnManager');

const playerPath1 = '/TemplateScripts/Player/Player.cs';
const playerPath2 = '/TemplateScripts/Player/PlayerMovement.cs';
const playerPath3 = '/TemplateScripts/Player/PlayerCombat.cs';
const playerPath4 = '/TemplateScripts/Player/PlayerAnimation.cs';

const enemyPath1 = '/TemplateScripts/Enemy/BoomkinAI.cs';
const enemyPath2 = '/TemplateScripts/Enemy/SentinelAI.cs';
const enemyPath3 = '/TemplateScripts/Enemy/JuggernautAI.cs';

const otherPath1 = '/TemplateScripts/WeaponData/WeaponDataSO.cs';
const otherPath2 = '/TemplateScripts/Other/GenerateEnemies.cs';
const otherPath3 = '/TemplateScripts/Other/SpawnManager.cs';

async function loadScriptContent(url, placeholder) {
    const loadedFile = await fetch(url);
    const file = await loadedFile.text();
    placeholder.textContent = file;
}

// Load and display script content for each script
loadScriptContent(playerPath1, player1);
loadScriptContent(playerPath2, player2);
loadScriptContent(playerPath3, player3);
loadScriptContent(playerPath4, player4);

loadScriptContent(enemyPath1, enemy1);
loadScriptContent(enemyPath2, enemy2);
loadScriptContent(enemyPath3, enemy3);

loadScriptContent(otherPath1, other1);
loadScriptContent(otherPath2, other2);
loadScriptContent(otherPath3, other3);

// copy the respective script
function copyText(scriptID) {
    var script = document.getElementById(scriptID).textContent;
    navigator.clipboard.writeText(script);
}

// scroll to show entire script section
function scrollToPosition() {
    window.scrollTo({ top: 287, behavior: 'smooth'});
}
