// Сохраняем ссылку на DotNetHelper для глобального доступа
let searchPopupDotNetHelper = null;

function initSearchPopup(dotNetHelper) {
    searchPopupDotNetHelper = dotNetHelper;
    const searchButton = document.querySelector('.search-button');

    // Обработчик для кнопки открытия поиска
    if (searchButton) {
        // Удаляем старые обработчики, если есть
        const newButton = searchButton.cloneNode(true);
        searchButton.parentNode.replaceChild(newButton, searchButton);

        // Добавляем новый обработчик
        newButton.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            dotNetHelper.invokeMethodAsync('GetOpenFromJs');
        });
    }
}

// Функция для фокуса на элементе
function focusElement(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.focus();
        // Помещаем курсор в конец текста
        element.setSelectionRange(element.value.length, element.value.length);
    }
}

// Функции для показа/скрытия попапа
function showSearchPopup() {
    const searchPopup = document.querySelector('.search-popup');
    if (searchPopup) {
        searchPopup.classList.add('is-visible');
        focusElement('search-form');

        // Блокируем прокрутку фона
        document.body.style.overflow = 'hidden';
    }
}

function hideSearchPopup() {
    const searchPopup = document.querySelector('.search-popup');
    if (searchPopup) {
        searchPopup.classList.remove('is-visible');

        // Восстанавливаем прокрутку фона
        document.body.style.overflow = '';
    }
}

// Экспортируем функции для модульной системы
if (typeof module !== 'undefined' && module.exports) {
    module.exports = {
        initSearchPopup,
        focusElement,
        showSearchPopup,
        hideSearchPopup
    };
}