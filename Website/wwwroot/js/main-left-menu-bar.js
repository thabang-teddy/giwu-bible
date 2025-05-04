
function saveAppdata(bible, book, chapter) {
    if (bible == null && book == null && chapter == null) {
        localStorage.removeItem("AppData");
    }
    else {
        let AppData = {
            bible: bible,
            book: book,
            chapter: chapter
        };

        localStorage.setItem("AppData", JSON.stringify(AppData));
    }
}

function getAppdata() {
    return JSON.parse(localStorage.getItem("AppData"));
}

let localAppData = getAppdata();

// Data structure
$(".menu-nav").on("click", function () {
    let openMenu = $(this).attr("open-menu");
    let isBackBtn = $(this).attr("back-btn");
    $(".menu").removeClass("selected-menu-con");
    $("#" + openMenu).addClass("selected-menu-con");

    if (openMenu == "book-list-menu" && isBackBtn == null) {

        let bible = $(this).attr("bible-data-abbreviation");
        let bookList = $(this).attr("bible-data-list");

        if (openMenu != null && openMenu != "" && bookList != null && bookList != "") {

            let jsonBookList = JSON.parse(bookList);

            AddBooks(bible, jsonBookList);
        }
        saveAppdata(bible, null, null);
    }
    else
    {
        let localAppData = saveAppdata();
        if (openMenu == "bible-list-menu" && isBackBtn != null && isBackBtn == "True") {
            saveAppdata(null, null, null);
        }

        if (openMenu == "book-list-menu" && localAppData != null && localAppData.bible != null && isBackBtn != null && isBackBtn == "True") {
            saveAppdata(localAppData.bible, null, null);
        }
    }

});

function AddBooks(bible, bookList) {

    $('#booksList').html("")

    $('#booksList').append('<p class="menu-item rounded selected-menu-item">' + bible + ' - Books</p>');

    for (var i = 0; i < bookList.length; i++) {
        $('#booksList').append('<div class="menu-item menu-nav" open-menu="chapter-list-menu" bible="' + bible + '" book="' + bookList[i].Book + '" book-name="' + bookList[i].Name + '" chapter-count="' + bookList[i].ChapterCount + '">' + bookList[i].Book + ' - ' + bookList[i].Name + ' (' + bookList[i].ChapterCount + ')</div>')
    }

    $('#booksList').find('.menu-nav').on("click", function () {
        let openMenu = $(this).attr("open-menu");
        $(".menu").removeClass("selected-menu-con");
        $("#" + openMenu).addClass("selected-menu-con");

        let bible = $(this).attr("bible");
        let book = JSON.parse($(this).attr("book"));
        let bookName = $(this).attr("book-name");
        let chapterCount = $(this).attr("chapter-count");

        AddCapters(bible, book, chapterCount, bookName);
        saveAppdata(bible, book, null);
    })
}

function AddCapters(bible, book, chapterCount, bookName = null) {
    $('#chaptersList').html("");

    $('#chaptersList').append('<p class="menu-item rounded selected-menu-item">' + bible + ' - ' + bookName + ' - Chapters</p>');

    for (var i = 0; i < chapterCount; i++) {
        let chapterNumber = i + 1;
        let isSelectedMenuItem = localAppData != null && bible == localAppData.bible && book == localAppData.book && chapterNumber == localAppData.chapter ? "selected-menu-item" : "";

        $('#chaptersList').append('<a href="/Home/Read/' + bible + '/' + book + '/' + chapterNumber + '" class="menu-item menu-nav rounded ' + isSelectedMenuItem + '" bible="' + bible + '" book="' + book + '" chapter="' + chapterNumber + '">Chapter ' + chapterNumber + '</a>');
    }

    $('#chaptersList').find('.menu-nav').on("click", function () {
        let bible = $(this).attr("bible");
        let book = JSON.parse($(this).attr("book"));
        let chapter = JSON.parse($(this).attr("chapter"));

        saveAppdata(bible, book, chapter);
    })
}

if (localAppData != null && localAppData.bible != null) {

    let bibleInfoRoot = $("#biblesList .menu-nav[bible-data-abbreviation='" + localAppData.bible + "']");

    if (bibleInfoRoot.length) {
        const fields = [
            "abbreviation", "name", "url", "read", "image",
            "publisher", "copyright",
            "language", "about", "other-info"
        ];

        fields.forEach(field => {
            const value = bibleInfoRoot.attr(`bible-data-${field}`) || "";

            if (field === "url") {
                $(".bible-data-url").attr("href", value);
            } else if (field === "read") {
                $(".bible-data-read").attr("href", value);
            } else if (field === "image") {
                //$(".bible-data-image").attr("src", value);
            } else {
                $(`.bible-data-${field}`).text(value);
            }
        });
    }

    let bookList = bibleInfoRoot.attr("bible-data-list");

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

                    if (localAppData.bible != null && localAppData.book != null && localAppData.chapter != null) {
                        let bibleDataRead = "/Home/Read/" + localAppData.bible + "/" + localAppData.book + "/" + localAppData.chapter;
                        $(".bible-data-read").attr("href", bibleDataRead).removeClass("d-none");
                    }

                    let bookName = selectedBook.Name;

                    AddCapters(localAppData.bible, localAppData.book, selectedBook.ChapterCount, bookName);
                    //updateBreadcrumbslink(localAppData.bible, selectedBook., localAppData.chapter);
                }

            }
        }
    }
}

function updateBreadcrumbslink(bible, book, chapter) {

    let breadcrumbslink = "";

    if (bible != null) {
        breadcrumbslink = breadcrumbslink + bible;
    }
    
    if (book != null) {
        breadcrumbslink = breadcrumbslink + book;
    }
    
    if (chapter != null) {
        breadcrumbslink = breadcrumbslink + chapter;
    }

    $("#breadcrumbs-con").text(breadcrumbslink);
}

