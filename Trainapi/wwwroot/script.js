const apiBaseUrl = "http://localhost:5023/api/train";
const loginUrl = "http://localhost:5023/api/User/login";
const registerUrl = "http://localhost:5023/api/User/register";

let token = "";


async function register() {
    const name = document.getElementById("regName").value;
    const email = document.getElementById("regEmail").value;
    const password = document.getElementById("regPassword").value;

    const response = await fetch(registerUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, email, password })
    });

    if (response.ok) {
        alert("Registration successful! Please login.");
    } else {
        alert("Registration failed. Try again.");
    }
}


async function login() {
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch(loginUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
    });

    if (response.ok) {
        token = await response.text();
        alert("Login successful! Token stored.");
    } else {
        alert("Login failed! Check credentials.");
    }
}

function displayTrains(trains) {
    const trainList = document.getElementById("trainList");
    trainList.innerHTML = "";

    trains.forEach(train => {
        const row = `
            <tr>
                <td>${train.id}</td>
                <td>${train.Trainno || train.trainno || train.trainNo}</td>  <!-- Fix Here -->
                <td>${train.PName || train.pname || train.pName}</td>
                <td>${train.SeatDetails || train.seatdetails || train.seatDetails}</td>
            </tr>
        `;
        trainList.innerHTML += row;
    });
}



async function getAllTrains() {
    const response = await fetch(apiBaseUrl, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
        }
    });

    console.log("Response Status:", response.status);

    if (response.ok) {
        const data = await response.json();
        console.log("Fetched Trains:", data);
        displayTrains(data);
    } else {
        console.error("Error Fetching Trains:", await response.text());
        alert("Failed to fetch trains.");
    }
}


async function getTrainById() {
    const id = document.getElementById("trainId").value;
    const response = await fetch(`${apiBaseUrl}/${id}`, {
        method: "GET",
        headers: { "Authorization": `Bearer ${token}` }
    });

    if (response.ok) {
        const data = await response.json();
        alert(JSON.stringify(data));
    } else {
        alert("Train not found.");
    }
}
