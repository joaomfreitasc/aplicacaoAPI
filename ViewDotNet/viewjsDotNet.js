async function analyzeImage() {
    const endpoint = 'http://localhost:5000/api/computerVision/analyze';

    const imageInput = document.getElementById('imageInput');
    const file = imageInput.files[0];

    if (!file) {
        alert('Favor selecionar a imagem.');
        return;
    }

    try {
        const formData = new FormData();
        formData.append('file', file);
        const response = await fetch(endpoint, {
            method: 'POST',
            body: formData
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
