var maincat = document.getElementsByClassName("maincat")

let active_item = document.getElementById(sessionStorage.getItem("active_item_id"))
console.log(active_item)

function show() {
    if (active_item == null) {
        console.log("undef")
        let subcat = this.getElementsByClassName("subcat")[0]
        subcat.className = "subcat-show"
        active_item = subcat
        sessionStorage.setItem("active_item_id", subcat.id)
    }
    else {
        if (active_item != this) {
            active_item.className = "subcat"
            let subcat = this.getElementsByClassName("subcat")[0]
            subcat.className = "subcat-show"
            active_item = subcat
            sessionStorage.setItem("active_item_id", subcat.id)
        }

    }

    
}


Array.from(maincat).forEach(function (item) {
    item.addEventListener("click", show, false)
})


