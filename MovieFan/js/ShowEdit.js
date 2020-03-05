// showedit.js: js needed for the Show and Edit view
// Author: XCL
// March 2020

document.addEventListener('DOMContentLoaded', function () {

    // Toggle Show/Edit movie
    cmdEdit.addEventListener('click', function () {
        divShow.classList.add('d-none')
        divEdit.classList.remove('d-none')
    })

    cmdCancel.addEventListener('click', function () {
        divShow.classList.remove('d-none')
        divEdit.classList.add('d-none')
    })

})
