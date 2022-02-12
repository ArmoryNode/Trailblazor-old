export function topbarMouseDown(event: MouseEvent, windowElement: HTMLElement, containerElement?: HTMLElement): void {
    if (typeof windowElement.style === "undefined")
        throw "Parameter `windowElement` is not an HTML element";

    containerElement = containerElement ?? document.body;

    if (typeof containerElement.style === "undefined")
        throw "Parameter `containerElement` is not an HTML element";

    const initialMouseX: number = event.clientX;
    const initialMouseY: number = event.clientY;

    const offsetLeft: number = initialMouseX - windowElement.getBoundingClientRect().left;
    const offsetTop: number = initialMouseY - windowElement.getBoundingClientRect().top;

    const dragStart = (e: MouseEvent): void => {
        const windowElementWidth: number = windowElement.getBoundingClientRect().width;
        const windowElementHeight: number = windowElement.getBoundingClientRect().height;

        const containerWidth: number = containerElement.getBoundingClientRect().width;
        const containerHeight: number = containerElement.getBoundingClientRect().height;

        const currentMouseX: number = e.clientX;
        const currentMouseY: number = e.clientY;

        let currentX: number = currentMouseX - offsetLeft;
        let currentY: number = currentMouseY - offsetTop;

        if (currentX < 0)
            currentX = 0;

        if (currentY < 0)
            currentY = 0;

        if (currentX + windowElementWidth > containerWidth)
            currentX = containerWidth - windowElementWidth;

        if (currentY + windowElementHeight > containerHeight)
            currentY = containerHeight - windowElementHeight;

        windowElement.style.left = currentX + "px";
        windowElement.style.top = currentY + "px";
    };

    const dragEnd = (): void => {
        document.removeEventListener('mousemove', dragStart);
        document.removeEventListener('mouseup', dragEnd);
    };

    document.addEventListener('mousemove', dragStart);
    document.addEventListener('mouseup', dragEnd);
}