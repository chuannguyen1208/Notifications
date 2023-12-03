const blobToBase64 = async blob => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result)
        reader.error = (err) => reject(err)
        reader.readAsDataURL(blob)
    })
}

const setInnerHtml = (element, html) => {
    element.innerHTML = html;
}

window.convertBlobURLToBase64 = async (url) => {
    const response = await fetch(url)
    const blob = await response.blob();
    const imageBase64 = await blobToBase64(blob)
    return imageBase64;
};

window.setInnerHtml = setInnerHtml;