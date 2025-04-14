let bookIndex = 0;

function addBibleBook(book = 0, name = "", chapterCount = 0) {
    const $template = $('.hidden .accordion-item').clone();

    // Update all the `base` keys to the correct index
    $template.attr('id', `accordion-item-${bookIndex}`);
    $template.find('.accordion-button')
        .attr('data-bs-target', `#flush-collapse-${bookIndex}`)
        .attr('aria-controls', `flush-collapse-${bookIndex}`)
        .html(`${book} - ${name} (${chapterCount})`);

    $template.find('.accordion-collapse')
        .attr('id', `flush-collapse-${bookIndex}`)
        .attr('data-bs-parent', '#bible-book-list')
        .removeClass('show');

    // Update input fields
    $template.find('input[id*="BookList_base__Book"]')
        .attr('id', `BookList_${bookIndex}__Book`)
        .attr('name', `BookList[${bookIndex}].Book`)
        .val(book);

    $template.find('input[id*="BookList_base__Name"]')
        .attr('id', `BookList_${bookIndex}__Name`)
        .attr('name', `BookList[${bookIndex}].Name`)
        .val(name);

    $template.find('input[id*="BookList_base__ChapterCount"]')
        .attr('id', `BookList_${bookIndex}__ChapterCount`)
        .attr('name', `BookList[${bookIndex}].ChapterCount`)
        .val(chapterCount);

    // Update delete button's attribute
    $template.find('button.btn-danger').attr('book-index', bookIndex);

    // Append the updated clone to the DOM
    $('#bible-book-list').append($template);

    bookIndex++;
}

// Example: Add a new book on button click
$(document).on('click', '#add-bible-book-btn', function () {
    addBibleBook();
});

// Optional: handle delete
$(document).on('click', '.btn-danger[book-index]', function () {
    $(this).closest('.accordion-item').remove();
});