getElementById("search").addEventListener("keydown", event => {
    if (event.isComposing || event.key == "Enter") {
        return;
    }
    // do something
});