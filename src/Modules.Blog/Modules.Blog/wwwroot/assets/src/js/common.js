export const blobUrlToBase64 = async url => new Promise(resolve => {
    fetch(url)
        .then(res => res.blob())
        .then(blob => {
            const fileReader = new FileReader();
            fileReader.addEventListener('load', () => resolve(fileReader.result));
            fileReader.readAsDataURL(blob);
        });
});

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

window.blobUrlToBase64 = blobUrlToBase64;
window.toast = toast;
