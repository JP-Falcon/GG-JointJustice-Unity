(async () => {
    body = await (await fetch('https://docs.google.com/document/d/1EYMbP8Qm3ySfpjey6LPC4lWSdLtgEIfi1Gi8rJFkRKU/pub')).text()
    body = body.split('doc-content">')[1].split('</div>')[0];
    body = body.replace(/\/p><p/g, '/p><br><p');
    body = body.replace(/<[^>]*>/g, function (match) {
        return match === '<br>' ? match : '';
    });
    body = body.replace(/<br>/g, '\n');
    body = body.replace(/&([^&;]*);/g, 
        tag => ({
            '&amp;': '&',
            '&lt;': '<',
            '&gt;': '>',
            '&#39;': "'",
            '&quot;': '"'
          }[tag]));
    // google renders tabs as 8 spaces; restore them
    body = body.replace(/        /g, "\t");
    // replace non-breaking space with regular space
    body = body.replace(/\xa0/g, ' ');
    require('fs').writeFileSync('./unity-ggjj/Assets/Resources/InkDialogueScripts/CaseDemo/2-1-FullScene.ink', body.trim());
})();