const tagTitleModal = document.getElementById("tagTitleModal");
const spanError = document.getElementById("spanError");
const txtSearch = document.getElementById("txtSearch");
const btnSalir = document.getElementById("btnSalir");
const divResultSearch = document.getElementById("divResultSearch");


let myData = null;

txtSearch.onkeyup = () => {
	spanError.innerHTML = "";
};

const abrirModalBusqueda = (titleModal) => {
	let modalBuscarCliente = new bootstrap.Modal(document.getElementById("modalBusquedaGeneral"));

	tagTitleModal.innerHTML = titleModal === "" ? "Buscar..." : titleModal;
	divResultSearch.innerHTML = "";
	spanError.innerHTML = "";
	txtSearch.value = "";
	modalBuscarCliente.show();
}

const buscarApi = async () => {
	myData = null;
	const valSearch = document.getElementById("txtSearch").value;
	if (valSearch === "") {
		spanError.innerHTML = "Ingrese un texto de búsqueda";
		return;
	}
	const resp = await fetch(`${urlApi}?textSearch=${valSearch}`, {
		method: "GET",
		headers: {
			"Content-Type": "application/json"
		}
	})
	const { data, msgError } = await resp.json();


	if (msgError) {
		spanError.innerHTML = msgError;
		return;
	}

	myData = [...data];

	contruyeTabla(data);

	// construye la tabla de resultados
	console.log(data);

}

const contruyeTabla = (data) => {

	let tabla = `<table class='table table-striped'>
								<thead class='table-dark'  >
								<tr>
								<td>Descripción</td>
								</tr>
								</thead>
								<tbody>`;

	data = data.map(r => {

		return `<tr>

								<td onclick="filaSeleccionada('${r.id}')" class='fila-busqueda' >
								${r.descripcion}
								</td>
								</tr>`;

	});

	tabla = tabla + data.join("");
	tabla = tabla + `</tbody></table>`;
	//
	divResultSearch.innerHTML = tabla;
}

//const filaSeleccionada = (id) => {
//	//buscar en myData el valor adicional
//	//console.log(myData);
//	myDataResult = myData.find(x => {
//		return x.id === id.toString()
//	})

//	btnSalir.click();
//}

const filaSeleccionada = (id) => {

	const mySelectedRow = myData.find(x => {
		return x.id === id.toString()
	})

	mySelectedRow.tipoBusqueda = urlApi;

	asignarValores(mySelectedRow);

	btnSalir.click();	
}