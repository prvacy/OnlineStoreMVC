var maincat = document.getElementsByClassName("maincat")

let active_item = document.getElementById(sessionStorage.getItem("active_item_id"))
console.log(active_item)

//function show() {
//    if (!active_item) {
//        let subcat = this.firstElementChild.nextElementSibling;
//        subcat.classList.add("subcat-show");
//        active_item = subcat
//        sessionStorage.setItem("active_item_id", subcat.id)

//    }
//    else {
//        if (active_item != this) {
//            console.log('xep')
//            active_item.className = "subcat"
//            let subcat = this.getElementsByClassName("subcat")[0]
//            subcat.className = "subcat-show"
//            active_item = subcat
//            sessionStorage.setItem("active_item_id", subcat.id)
//        }

//    }

    
//}

filter.addEventListener('click', e => {
    var target = e.target;
    if (target.closest('.maincat')) {
        if (target.tagName == "P") {
            target.nextElementSibling.classList.toggle('subcat-show');
            return;
        }
        target.children[1].classList.toggle('subcat-show');
    }
})

//Array.prototype.slice.call(maincat).forEach(function (item) {
//    item.addEventListener("click", show, false)
//})


