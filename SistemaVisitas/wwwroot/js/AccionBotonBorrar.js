const accionBorrar = async (id, url, lblNombre) => {

    //muestra mensaje de confirmacion

    const pregunta = await Swal.fire({
        title: 'Eliminar',
        text: `¿Desea eliminar "${lblNombre}"?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Eliminar'
    });

    if (pregunta.isConfirmed) {

        const error = await apiEliminar(id, url)
        //debugger;
        if (error == "") {
           await Swal.fire(
                'Eliminado',
                'El registro fue eliminado!.',
                'success'
            )
            location.reload();
        } else {
            Swal.fire(
                'Error',
                error,
                'error'
            )
        }
        

    } 
     
}

const apiEliminar = async (id, url) => {
    const resp = await fetch(`${url}/${id}`, {
        method: "DELETE" 
    })
     
    

    //console.log(resp);
    
    if (resp.ok) {
        return "";
    } else {
        const dataResp = await resp.json();
        return dataResp.msgError;
    }
}