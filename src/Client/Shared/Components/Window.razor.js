export function topbarMouseDown(event, windowElement, containerElement) {
    if (typeof windowElement.style === "undefined")
        throw "Parameter `windowElement` is not an HTML element";
    containerElement = containerElement !== null && containerElement !== void 0 ? containerElement : document.body;
    if (typeof containerElement.style === "undefined")
        throw "Parameter `containerElement` is not an HTML element";
    const initialMouseX = event.clientX;
    const initialMouseY = event.clientY;
    const offsetLeft = initialMouseX - windowElement.getBoundingClientRect().left;
    const offsetTop = initialMouseY - windowElement.getBoundingClientRect().top;
    const dragStart = (e) => {
        const windowElementWidth = windowElement.getBoundingClientRect().width;
        const windowElementHeight = windowElement.getBoundingClientRect().height;
        const containerWidth = containerElement.getBoundingClientRect().width;
        const containerHeight = containerElement.getBoundingClientRect().height;
        const currentMouseX = e.clientX;
        const currentMouseY = e.clientY;
        let currentX = currentMouseX - offsetLeft;
        let currentY = currentMouseY - offsetTop;
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
    const dragEnd = () => {
        document.removeEventListener('mousemove', dragStart);
        document.removeEventListener('mouseup', dragEnd);
    };
    document.addEventListener('mousemove', dragStart);
    document.addEventListener('mouseup', dragEnd);
}
