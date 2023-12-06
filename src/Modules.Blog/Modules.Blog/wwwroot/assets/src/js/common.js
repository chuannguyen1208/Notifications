export const blobToBase64 = async blob => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result)
        reader.error = (err) => reject(err)
        reader.readAsDataURL(blob)
    })
}

export const convertBlobURLToBase64 = async (url) => {
    const response = await fetch(url)
    const blob = await response.blob();
    const imageBase64 = await blobToBase64(blob)
    return imageBase64;
};

export const toast = (text, type = '' | 'text-success' | 'text-danger', closeAfterMs = 2000) => {
    const toast = document.querySelector(".toast");

    if (toast) {

        const body = toast.querySelector(".toast-body");
        body.innerHTML = text;
        body.classList.add(type);

        toast.classList.add('show');
        setTimeout(function () {
            toast.classList.remove('show');
        }, closeAfterMs);
    }
}

window.convertBlobURLToBase64 = convertBlobURLToBase64;
window.toast = toast;
