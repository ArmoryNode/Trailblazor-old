export function topbarMouseDown(event: MouseEvent, windowElement: HTMLElement, containerElement: HTMLElement = null): Promise<HTMLElement> {
    return new Promise((resolve, reject) => {
        if (typeof windowElement.style === "undefined")
            reject("Parameter `windowElement` is not an HTML element");

        containerElement = containerElement ?? document.body;

        if (typeof containerElement.style === "undefined")
            reject("Parameter `containerElement` is not an HTML element");

        windowElement.querySelector('.tb-window-topbar').classList.add('tb-dragging');

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
            windowElement.querySelector('.tb-window-topbar').classList.remove('tb-dragging');

            document.removeEventListener('mousemove', dragStart);
            document.removeEventListener('mouseup', dragEnd);

            resolve(windowElement);
        };

        document.addEventListener('mousemove', dragStart);
        document.addEventListener('mouseup', dragEnd);
    });
}

export function resizeSideHandleMouseDown(event: MouseEvent, windowElement: HTMLElement, side: 'left' | 'right' | 'top' | 'bottom', containerElement: HTMLElement = null, minWidth: number = 300, minHeight: number = 300) {
    if (typeof windowElement.style === "undefined")
        throw "Parameter `windowElement` is not an HTML element";

    containerElement = containerElement ?? document.body;

    const windowBoundingClientRect = windowElement.getBoundingClientRect();
    const containerBoundingClientRect = containerElement.getBoundingClientRect();

    let initialWidth = windowBoundingClientRect.width;
    let initialHeight = windowBoundingClientRect.height;

    let initialX = windowBoundingClientRect.left;
    let initialY = windowBoundingClientRect.top;

    let initialMouseX = event.clientX;
    let initialMouseY = event.clientY;

    const resizeWindow = (e: MouseEvent): void => {
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
                    // confine in container/body
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
                    // confine in container/body
                    if (handleY > containerHeight) {
                        windowElement.style.height = `${height}px`;
                        break;
                    }
                    windowElement.style.height = `${height + handleY}px`;
                }
                break;
        }
    };

    const stopResize = (e: MouseEvent): void => {
        document.removeEventListener('mouseup', stopResize);
        document.removeEventListener('mousemove', resizeWindow);
    };

    document.addEventListener('mousemove', resizeWindow);
    document.addEventListener('mouseup', stopResize);
}