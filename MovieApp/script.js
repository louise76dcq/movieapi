$(document).ready(function () {
    fetch("http://localhost:5170/GetAll")
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log(data); 
            populateTable(data); 
        })
        .catch(error => {
            console.error("Erreur lors de la récupération des données :", error);
        });

   
    function populateTable(data) {
        const tbody = $("table tbody");
        tbody.empty(); 

        
        data.value.forEach(item => {
            const row = `
                <tr>
                    <td>${item.movieName}</td>
                    <td>${item.movieDirector}</td>
                    <td>${item.movieYear}</td>
                    <td>
                    <button onClick=deleteMovie(${item.id})>Supprimer</button>
                    <button type="submit" onclick= >Modifier</button>


                    </td>
                </tr>
            `;
            tbody.append(row); 
        });
    }
});

function deleteMovie(id){
    fetch("http://localhost:5170/api/Movie/Delete?id=" + id, { method: "DELETE" })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
           
            document.querySelector(`#row-${id}`).remove();
        })
        .catch(error => {
            console.error("Erreur lors de la suppression :", error);
        });
}

function postMovie() {
    let movie = document.getElementById("Movie").value;
    let director = document.getElementById("Director").value;
    let year = document.getElementById("Year").value;

    console.log("Année :", year);

    const movieData = {
        id: 0, 
        movieName: movie,
        movieDirector: director,
        movieYear: parseInt(year, 10),
    };

    console.log("Données envoyées :", movieData);

    fetch("http://localhost:5170/api/Movie/CreateEdit", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(movieData),
    })
        .then((response) => {
            console.log("Réponse brute :", response);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then((data) => {
            console.log("Réponse du serveur :", data);
            alert("Film ajouté avec succès !");

            addMovieToTable(data);

            document.getElementById("Movie").value = "";
            document.getElementById("Director").value = "";
            document.getElementById("Year").value = "";
        })
        .catch((error) => {
            console.error("Erreur lors de l'ajout du film :", error);
            alert("Une erreur est survenue lors de l'ajout du film !");
        });
}


function addMovieToTable(movie) {
    const tableBody = document.getElementById("moviesTableBody");

    const row = document.createElement("tr");

    row.innerHTML = `
        <td>${movie.id}</td>
        <td>${movie.movieName}</td>
        <td>${movie.movieDirector}</td>
        <td>${movie.movieYear}</td>
    `;

    tableBody.appendChild(row);
}




function modifyMovie()  {
    


    const id = document.getElementById("id").value;
    const movieName = document.getElementById("movieName").value;
    const movieDirector = document.getElementById("movieDirector").value;
    const movieYear = document.getElementById("movieYear").value;

    const movieData = {
        id : id,
        movieName: movieName,
        movieDirector: movieDirector,
        movieYear: parseInt(movieYear, 10),
    };

    
    fetch(`http://localhost:5170/api/Movie/Edit`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(movieData) 
    })

    .then(response => response.json())
    .then(data => {
        console.log(data)
        if (data.success) {
            alert('Film modifié avec succès!');
           /* window.location.href = 'index.html'; */
        } else {
            alert('Erreur lors de la modification du film');
        }
    })
    .catch(error => {
        console.error('Erreur dans la promesse:', error);
        alert('Erreur de connexion');
    });

}


