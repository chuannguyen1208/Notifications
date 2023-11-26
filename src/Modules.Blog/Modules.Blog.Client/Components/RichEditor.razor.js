export class BlazorQuill {
    static quill;

    static init(element, reviewElement) {
        console.log('quill init...');
        var toolbarOptions = [
            ['bold', 'italic', 'underline', 'strike'],
            [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
            ['blockquote', 'code-block'],
            ['link', 'image'],
            [{ 'color': [] }, { 'background': [] }]
        ];

        this.quill = new Quill(element, {
            modules: {
                toolbar: toolbarOptions
            },
            theme: 'snow',
        });
    }

    static listen(dotnetInstance) {
        this.quill.on('text-change', function () {
            const content = this.quill.root.innerHTML;
            if (dotnetInstance) {
                dotnetInstance.invokeMethodAsync("EditorCallback", content);
            }
        }.bind(this));
    }
}

window.BlazorQuill = BlazorQuill;