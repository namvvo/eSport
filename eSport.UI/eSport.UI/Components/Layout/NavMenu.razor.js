export function openMenu(event, item) {
    let parent = event.parentNode;
    let sibling = parent === null || parent === void 0 ? void 0 : parent.firstChild;
    console.log('abc')
    if (item.data.length > 0) {
        while (sibling) {
            if (sibling.nodeType === 1 && sibling !== event) {
                sibling.classList.remove("open");
            }
            sibling = sibling.nextSibling;
        }
        var currentTaget = event; //.currentTarget as Element
        if (currentTaget.classList.contains("open"))
            currentTaget === null || currentTaget === void 0 ? void 0 : currentTaget.classList.remove("open");
        else {
            currentTaget === null || currentTaget === void 0 ? void 0 : currentTaget.classList.add("open");
        }
    }
    if (item.data.length === 0) {
        //menu.value.forEach((e) => (e.selected = false));
        item.selected = true;
    }
}
