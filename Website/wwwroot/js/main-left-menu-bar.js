
// Data structure
$(".menu-nav").on("click", function () {
    let openMenu = $(this).attr("open-menu");
    let isBackBtn = $(this).attr("back-btn");
    $(".menu").removeClass("selected-menu-con");
    $("#" + openMenu).addClass("selected-menu-con");

    if (openMenu == "book-list-menu" && isBackBtn != null && isBackBtn != "True") {

        let bible = $(this).attr("bible");
        let bookList = $(this).attr("book-list");

        if (openMenu != null && openMenu != "" && bookList != null && bookList != "") {

            let jsonBookList = JSON.parse(bookList);

            AddBooks(bible, jsonBookList);
        }
    }
});

function AddBooks(bible, bookList) {

    $('#booksList').html("")

    for (var i = 0; i < bookList.length; i++) {
        $('#booksList').append('<div class="menu-item menu-nav" open-menu="chapter-list-menu" bible="' + bible + '" book="' + bookList[i].Book + '" chapter-count="' + bookList[i].ChapterCount + '">' + bookList[i].Book + ' - ' + bookList[i].Name + ' (' + bookList[i].ChapterCount + ')</div>')
    }

    $('#booksList').find('.menu-nav').on("click", function () {
        let openMenu = $(this).attr("open-menu");
        $(".menu").removeClass("selected-menu-con");
        $("#" + openMenu).addClass("selected-menu-con");

        let bible = $(this).attr("bible");
        let book = JSON.parse($(this).attr("book"));
        let chapterCount = $(this).attr("chapter-count");

        AddCapters(bible, book, chapterCount);
    })
}

function AddCapters(bible, book, chapterCount) {

    $('#chaptersList').html("")

    for (var i = 0; i < chapterCount; i++) {
        let chapterNumber = i + 1;
        $('#chaptersList').append('<div class="menu-item menu-nav" bible="' + bible + '" book="' + book + '" chapter="' + chapterNumber + '">Chapter ' + chapterNumber + '</div>')
    }
}