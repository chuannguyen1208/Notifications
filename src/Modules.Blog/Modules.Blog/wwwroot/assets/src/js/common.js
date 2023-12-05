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

const toast = (text, textColorClass = "") => {
    const toast = document.querySelector(".toast");
    if (toast) {

        const body = toast.querySelector(".toast-body");
        body.innerHTML = text;
        body.classList.add(textColorClass);

        toast.classList.add('show');
        setTimeout(function () {
            toast.classList.remove('show');
        }, 3000);
    }
}

window.convertBlobURLToBase64 = async (url) => {
    const response = await fetch(url)
    const blob = await response.blob();
    const imageBase64 = await blobToBase64(blob)
    return imageBase64;
};

window.setInnerHtml = setInnerHtml;
window.toast = text => toast(text);
window.toastSuccess = text => toast(text, "text-success");
window.toastError = text => toast(text, "text-danger");