
// Data structure
const bibleData = {
    "King James Bible": {
        "Genesis": 50,
        "Exodus": 40
    },
    "New International Version": {
        "Matthew": 28,
        "Mark": 16
    }
};

$(".menu-nav").on("click", function () {
    let openMenu = $(this).attr("open-menu");
    console.log(openMenu);
    $(".menu").removeClass("selected-menu-con");
    $("#" + openMenu).addClass("selected-menu-con");
});

