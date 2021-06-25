const parksSection = document.querySelector('#parks-section');
const searchInput = document.querySelector('#search-input');
const searchBtn = document.querySelector('#search-btn');


function getParks() {
    parksSection.innerHTML = null;

    fetch('./parksJson')
        .then(response => response.json())
        .then(data => {
            console.log('data', data)
            createContent(data.allParks);
        })
}

function searchParks(searchString) {
    parksSection.innerHTML = null;
    const queryString = setQueryString(searchString);

    fetch(queryString)
        .then(response => response.json())
        .then(data => {
            console.log('data', data)
            createContent(data.allParks);
        })
}

function createContent(allParks) {
    if (allParks.length === 0) {
        imgContainer.innerHTML = `<h2 class="no-results">No Results Found<h2>`;
    } else {
        allParks.forEach(park => {
            createCards(park);
        });
    }
}

function createCards(park) {
    const parkCard = document.createElement('div');
    parkCard.classList.add('container');

    const parkDivider = document.createElement('hr');

    const parkNameRow = document.createElement('div');
    parkNameRow.classList.add('row');

    const parkBoroughRow = document.createElement('div');
    parkBoroughRow.classList.add('row');

    const parkAcresRow = document.createElement('div');
    parkAcresRow.classList.add('row');

    const parkDescriptionRow = document.createElement('div');
    parkDescriptionRow.classList.add('row');

    const parkName = document.createElement('h3');
    parkName.textContent = park.parkName;

    const boroughName = document.createElement('h5');
    boroughName.textContent = park.borough;

    const acres = document.createElement('h5');
    acres.textContent = park.acres;

    const parkDescriptionTitle = document.createElement('h5');
    parkDescriptionTitle.textContent = 'Description: ';

    const parkDescriptionSection = document.createElement('div');
    parkDescriptionSection.innerHTML = park.description;

    parkNameRow.appendChild(parkName);
    parkBoroughRow.appendChild(boroughName);
    parkAcresRow.appendChild(acres);
    parkDescriptionRow.appendChild(parkDescriptionTitle);
    parkDescriptionRow.appendChild(parkDescriptionSection);

    parkCard.appendChild(parkDivider);
    parkCard.appendChild(parkNameRow);
    parkCard.appendChild(parkBoroughRow);
    parkCard.appendChild(parkAcresRow);
    parkCard.appendChild(parkDescriptionRow);

    parksSection.appendChild(parkCard);
}

function setQueryString(searchString) {
    if (searchString) {
        return `./parksJson/?search=${searchString}`;
    } else {
        return `./parksJson`;
	}
}

searchInput.addEventListener('keyup', (event) => {
    if (event.keyCode === 13) {
        let searchValue = searchInput.value;
        searchParks(searchValue);
    }
});

searchBtn.addEventListener('click', () => {
    let searchValue = searchInput.value;
    searchParks(searchValue);
});

getParks();
