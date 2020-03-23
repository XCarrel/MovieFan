// Add event listeners to rows tagged with class 'clickable'
// The address to go to is in its data-href attribute
document.addEventListener('DOMContentLoaded', function () {
    els = document.getElementsByClassName('clickable')
    Array.prototype.forEach.call(els, function (el) {
        el.addEventListener('click', function (e) {
            window.location = e.target.parentNode.getAttribute('data-href')
        })
    })
})