
// Data structure
$(".menu-nav").on("click", function () {
    let openMenu = $(this).attr("open-menu");
    let isBackBtn = $(this).attr("back-btn");
    $(".menu").removeClass("selected-menu-con");
    $("#" + openMenu).addClass("selected-menu-con");

    if (openMenu == "book-list-menu" && isBackBtn == null) {

        let bible = $(this).attr("bible");
        let bookList = $(this).attr("book-list");

        if (openMenu != null && openMenu != "" && bookList != null && bookList != "") {

            let jsonBookList = JSON.parse(bookList);

            AddBooks(bible, jsonBookList);
        }
        saveSideBarData(bible, null, null);
    }
    else
    {
        let localAppData = getSideBarData();
        if (openMenu == "bible-list-menu" && isBackBtn != null && isBackBtn == "True") {
            saveSideBarData(null, null, null);
        }

        if (openMenu == "book-list-menu" && isBackBtn != null && isBackBtn == "True") {
            saveSideBarData(localAppData.bible, null, null);
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
        saveSideBarData(bible, book, null);
    })
}

function AddCapters(bible, book, chapterCount) {
    $('#chaptersList').html("");

    for (var i = 0; i < chapterCount; i++) {
        let chapterNumber = i + 1;
        $('#chaptersList').append('<div class="menu-item menu-nav" bible="' + bible + '" book="' + book + '" chapter="' + chapterNumber + '">Chapter ' + chapterNumber + '</div>');
    }

    $('#chaptersList').find('.menu-nav').on("click", function () {
        let bible = $(this).attr("bible");
        let book = JSON.parse($(this).attr("book"));
        let chapter = JSON.parse($(this).attr("chapter"));

        saveAppdata(bible, book, chapter);
    })
}

function saveSideBarData(bible,book,chapter) {
    let SideBarData = {
        bible: bible,
        book: book,
        chapter: chapter
    };

    localStorage.setItem("SideBarData", JSON.stringify(SideBarData));
}

function getSideBarData() {
    return JSON.parse(localStorage.getItem("SideBarData"));
}

function saveAppdata(bible, book, chapter) {
    let AppData = {
        bible: bible,
        book: book,
        chapter: chapter
    };

    localStorage.setItem("AppData", JSON.stringify(AppData));
}

function getAppdata() {
    return JSON.parse(localStorage.getItem("AppData"));
}

let localAppData = getSideBarData();

if (localAppData.bible != null) {

    let bookList = $("#biblesList").find(".menu-nav[bible='" + localAppData.bible + "']").attr("book-list");

    if (bookList != null && bookList != "") {

        let jsonBookList = JSON.parse(bookList);

        if (jsonBookList != null && jsonBookList.length > 0) {

            $(".menu").removeClass("selected-menu-con");
            $("#book-list-menu").addClass("selected-menu-con");

            AddBooks(localAppData.bible, jsonBookList);

            if (localAppData.book != null) {

                let selectedBook = jsonBookList.find(b => b.Book === localAppData.book);
                if (selectedBook != null && selectedBook.ChapterCount > 0) {

                    $(".menu").removeClass("selected-menu-con");
                    $("#chapter-list-menu").addClass("selected-menu-con");

                    AddCapters(localAppData.bible, localAppData.book, selectedBook.ChapterCount);
                }

            }
        }
    }
}



