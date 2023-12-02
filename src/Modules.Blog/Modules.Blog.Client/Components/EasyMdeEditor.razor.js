export class EasyMdeEditor {
    static easymde;

    static init(element) {
        this.easymde = new EasyMDE({
            element: element
        });
    }
}

window.EasyMdeEditor = EasyMdeEditor;