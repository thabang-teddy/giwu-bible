$(".book-list-item").on("click",function (e) {
	let bookNumber = $(this).attr("book-number");
	let bookChapterCount = $(this).attr("book-chapter-count");
	let bookName = $(this).attr("book-name");

    AddCapters(bookName, bookNumber, bookChapterCount);
});


function AddCapters(bookName, book, chapterCount) {
    $('#bible-book-name').html(bookName);
    $('#chaptersList').html('<span class="list-group-item list-group-item-action new-chapter-menu-nav" id="list-edit-chapter-list" data-bs-toggle="list" href="#list-edit-chapter" role="tab" aria-controls="list-edit-chapter" book="' + book + '">New Chapter</span>');

    for (var i = 0; i < chapterCount; i++) {
        let chapterNumber = i + 1;

        let chapterItem = '<span class="list-group-item list-group-item-action chapter-menu-nav" id="list-edit-chapter-list-' + chapterNumber + '" data-bs-toggle="list" href="#list-edit-chapter" role="tab" aria-controls="list-edit-chapter" book="' + book + '" chapter="' + chapterNumber + '">';
        chapterItem = chapterItem + "Chapter " + chapterNumber;
        chapterItem = chapterItem + "</span>";

        $('#chaptersList').append(chapterItem);
    }

    $('#chaptersList').find('.new-chapter-menu-nav').on("click", function () {
        let book = $(this).attr("book");
        let bibleBookId = $("#BibleBookId").val();
        $.get('/Bibles/GetChapterCreatePartialForm/' + bibleBookId + '/' + book, function (data) {
            $('#list-edit-chapter').html(data).fadeIn();
            SetFormSumition();
        });
    });

    $('#chaptersList').find('.chapter-menu-nav').on("click", function () {
        let book = $(this).attr("book");
        let chapter = $(this).attr("chapter");
        let bibleBookId = $("#BibleBookId").val();
        $.get('/Bibles/GetChapterEditPartialForm/' + bibleBookId + '/' + book + '/' + chapter, function (data) {
            $('#list-edit-chapter').html(data).fadeIn();
            SetFormSumition();
        });
    });
}

function SetFormSumition() {
    $('#chapterForm').on('submit', function (e) {
        e.preventDefault();

        // Trigger validation
        if (!$(this).valid()) {
            return; // Don't submit via AJAX if form is invalid
        }

        $.ajax({
            url: $(this).attr('action'),
            method: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (html) {
                $('#list-edit-chapter').html(html); // or use a div like #formResult
            },
            error: function (xhr) {
                alert('Something went wrong.');
                console.log(xhr.responseText);
            }
        });
    });
}