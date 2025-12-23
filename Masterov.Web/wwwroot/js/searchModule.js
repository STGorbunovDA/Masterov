// Модуль для отслеживания кликов вне поиска
export function setupClickTracking(dotNetHelper) {
    // Обработчик клика на документе
    document.addEventListener('click', function(event) {
        handleDocumentClick(event, dotNetHelper);
    }, true); // Используем capturing phase

    // Обработчик клавиши Escape
    document.addEventListener('keydown', function(event) {
        if (event.key === 'Escape') {
            handleEscapeKey(dotNetHelper);
        }
    });
}

function handleDocumentClick(event, dotNetHelper) {
    const searchPopup = document.querySelector('.search-popup');

    if (!searchPopup || !searchPopup.classList.contains('is-visible')) {
        return;
    }

    // Проверяем, был ли клик на элементах поиска
    const isClickOnSearchElements = isClickOnSearchRelatedElement(event.target);

    // Если клик не на элементах поиска, закрываем попап
    if (!isClickOnSearchElements) {
        dotNetHelper.invokeMethodAsync('HandleOutsideClick');
    }
}

function isClickOnSearchRelatedElement(element) {
    // Проверяем, является ли элемент или его родители частью поиска
    const searchElements = [
        '.search-popup-container',
        '.search-form',
        '.search-field',
        '.search-submit',
        '.cat-list',
        '.cat-list-item',
        '.category-link'
    ];

    // Проверяем текущий элемент
    for (const selector of searchElements) {
        if (element.matches(selector)) {
            return true;
        }
    }

    // Проверяем родителей
    let currentElement = element;
    while (currentElement && currentElement !== document.body) {
        for (const selector of searchElements) {
            if (currentElement.matches && currentElement.matches(selector)) {
                return true;
            }
        }
        currentElement = currentElement.parentElement;
    }

    return false;
}

function handleEscapeKey(dotNetHelper) {
    const searchPopup = document.querySelector('.search-popup');
    if (searchPopup && searchPopup.classList.contains('is-visible')) {
        dotNetHelper.invokeMethodAsync('HandleOutsideClick');
    }
}