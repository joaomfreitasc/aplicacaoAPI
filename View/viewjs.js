async function analyzeImage() {
    const subscriptionKey = 'df3f2516ce1c413c827e1f05b5c98a06'; // Substitua pela sua chave de API
    const endpoint = 'https://fastfutsite.cognitiveservices.azure.com/vision/v3.1/analyze'; // Substitua pelo seu endpoint
    const params = '?visualFeatures=Tags&language=pt';

    const imageInput = document.getElementById('imageInput');
    const file = imageInput.files[0];

    if (!file) {
        alert('Favor selecionar a imagem.');
        return;
    }

    try {
        const arrayBuffer = await file.arrayBuffer();
        const response = await fetch(endpoint + params, {
            method: 'POST',
            headers: {
                'Ocp-Apim-Subscription-Key': subscriptionKey,
                'Content-Type': 'application/octet-stream'
            },
            body: arrayBuffer
        });

        if (!response.ok) {
            throw new Error('Erro ao analisar imagem: ' + response.statusText);
        }

        const result = await response.json();
        displayTags(result.tags);
    } catch (error) {
        console.error('Erro:', error);
        alert('Erro ao analisar imagem: ' + error.message);
    }
}

function displayTags(tags) {
    const tableBody = document.querySelector('#tagsTable tbody');
    tableBody.innerHTML = ''; 

    tags.forEach(tag => {
        const row = document.createElement('tr');
        const tagCell = document.createElement('td');

        tagCell.textContent = tag.name;

        row.appendChild(tagCell);
        tableBody.appendChild(row);
    });
}
