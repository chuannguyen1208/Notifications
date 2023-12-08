export function onLoad() {
    const navs = document.querySelectorAll('.nav-portfolio');
    navs.forEach(nav => {
        nav.addEventListener('click', e => {
            e.preventDefault();
            const href = nav.getAttribute('href');
            window.location.href = href;
        });
    });
}

export function onUpdate() {
    console.log('Updated');
}

export function onDispose() {
    console.log('Disposed');
}
