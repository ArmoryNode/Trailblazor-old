export function topbarMouseDown(event, windowElement, containerElement = null) {
    if (typeof windowElement.style === "undefined")
        throw "Parameter `windowElement` is not an HTML element";
    containerElement = containerElement !== null && containerElement !== void 0 ? containerElement : document.body;
    if (typeof containerElement.style === "undefined")
        throw "Parameter `containerElement` is not an HTML element";
    windowElement.querySelector('.tb-window-topbar').classList.add('tb-dragging');
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
        windowElement.querySelector('.tb-window-topbar').classList.remove('tb-dragging');
        document.removeEventListener('mousemove', dragStart);
        document.removeEventListener('mouseup', dragEnd);
    };
    document.addEventListener('mousemove', dragStart);
    document.addEventListener('mouseup', dragEnd);
}
export function resizeSideHandleMouseDown(event, windowElement, side, containerElement = null, minWidth = 300, minHeight = 300) {
    if (typeof windowElement.style === "undefined")
        throw "Parameter `windowElement` is not an HTML element";
    containerElement = containerElement !== null && containerElement !== void 0 ? containerElement : document.body;
    const windowBoundingClientRect = windowElement.getBoundingClientRect();
    const containerBoundingClientRect = containerElement.getBoundingClientRect();
    let initialWidth = windowBoundingClientRect.width;
    let initialHeight = windowBoundingClientRect.height;
    let initialX = windowBoundingClientRect.left;
    let initialY = windowBoundingClientRect.top;
    let initialMouseX = event.clientX;
    let initialMouseY = event.clientY;
    const resizeWindow = (e) => {
        const width = initialWidth + (event.pageX - initialMouseX);
        const height = initialHeight + (event.pageY - initialMouseY);
        const handleX = e.clientX - initialMouseX;
        const handleY = e.clientY - initialMouseY;
        const containerWidth = containerBoundingClientRect.right;
        const containerHeight = containerBoundingClientRect.bottom;
        switch (side) {
            case 'left':
                if (e.pageX > 0 && width > minWidth + handleX) {
                    windowElement.style.width = `${width - handleX}px`;
                    windowElement.style.left = `${initialX + (e.pageX - initialMouseX)}px`;
                }
                break;
            case 'right':
                if (e.pageX < containerWidth && width > minWidth - handleX) {
                    if (handleX > containerWidth) {
                        windowElement.style.width = `${width}px`;
                        break;
                    }
                    windowElement.style.width = `${width + handleX}px`;
                }
                break;
            case 'top':
                if (e.pageY > 0 && height > minHeight + handleY) {
                    windowElement.style.height = `${height - handleY}px`;
                    windowElement.style.top = `${initialY + (e.pageY - initialMouseY)}px`;
                }
                break;
            case 'bottom':
                if (e.pageY < containerHeight && height > minHeight - handleY) {
                    if (handleY > containerHeight) {
                        windowElement.style.height = `${height}px`;
                        break;
                    }
                    windowElement.style.height = `${height + handleY}px`;
                }
                break;
        }
    };
    const stopResize = (e) => {
        document.removeEventListener('mouseup', stopResize);
        document.removeEventListener('mousemove', resizeWindow);
    };
    document.addEventListener('mousemove', resizeWindow);
    document.addEventListener('mouseup', stopResize);
}
