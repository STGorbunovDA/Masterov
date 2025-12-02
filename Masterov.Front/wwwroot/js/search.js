window.initSearchPopup = () => {
    const btn = document.querySelector('.search-button');
    const popup = document.querySelector('.search-popup');

    if (!btn || !popup) return;

    btn.addEventListener('click', function (e) {
        e.preventDefault();
        popup.classList.add('is-visible');
    });

    popup.addEventListener('click', function (e) {
        if (e.target === popup) {
            popup.classList.remove('is-visible');
        }
    });
};

// Функция для фокуса на элементе
function focusElement(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.focus();
    }
}

// Инициализация поиска
function initSearchPopup(dotNetHelper) {
    // Обработчик клика вне попапа
    document.addEventListener('click', function(event) {
        const searchPopup = document.querySelector('.search-popup');
        const searchButton = document.querySelector('.search-button');

        if (searchPopup && searchPopup.classList.contains('is-visible') &&
            !searchPopup.contains(event.target) &&
            (!searchButton || !searchButton.contains(event.target))) {
            dotNetHelper.invokeMethodAsync('CloseFromJS');
        }
    });

    // Обработчик ESC
    document.addEventListener('keydown', function(event) {
        if (event.key === 'Escape') {
            dotNetHelper.invokeMethodAsync('CloseFromJS');
        }
    });
}

// Функция для открытия поиска (для вызова из других мест)
function openSearchPopup() {
    const searchPopup = document.querySelector('.search-popup');
    const searchInput = document.getElementById('search-form');

    if (searchPopup) {
        searchPopup.classList.add('is-visible');
        if (searchInput) {
            searchInput.focus();
        }
    }
}