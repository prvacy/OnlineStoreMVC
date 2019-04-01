
var maincat = document.getElementsByClassName("maincat")

function show() {

    var subcat = this.getElementsByClassName("subcat")[0]

    if (subcat.style.display == "block") {
        subcat.style.display = "none"
        console.log("Hide s1")
    }
    else {
        subcat.style.display = "block"
        console.log("Show s1")
    }
}

//maincat.addEventListener("click", show2)

Array.from(maincat).forEach(function (item) {
    item.addEventListener("click", show, false)
})


