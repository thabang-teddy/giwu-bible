let book = "john";
let chapter = 1;
let version = "kjv";

function loadChapter() {
    $.get(`/Visitor/Home/Chapter`, { book, chapter, version }, function (data) {
        //$("#chapterTitle").text(data.reference);
        //$("#verses").empty();

        //data.verses.forEach(v => {
        //    $("#verses").append(`
        //        <p class="verse" data-verse="${v.verse}">
        //            <sup>${v.verse}</sup> ${v.text}
        //        </p>
        //    `);
        //});
    });
}

$(document).on("click", ".verse", function () {
    const verse = $(this).data("verse");
    $.get(`/Visitor/Home/Verse`, { book, chapter, verse, version }, function (data) {
        $("#parallel").html(`<p>${data.text}</p>`);
    });
});

$(document).ready(loadChapter);
