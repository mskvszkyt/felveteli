const data = [
    new Diak("98657574765", "Gipsz János", 50, 1),
    new Diak("12345678901", "Kovács Eszter", 43, 3),
    new Diak("98765432109", "Nagy Gábor", 2, 44),
    new Diak("55555555555", "Varga Anna", 13, 5),
    new Diak("77777777777", "Kis Péter", 3, 22),
]

const pgNotFound = document.getElementById("id-not-found-page")
const pgUserFound = document.getElementById("id-found-page")
const pgUserAdd = document.getElementById("create-student-page")

let order = {
    base: "nev",
    desc: true
}

let limit = 0

const SetOrder = (val) => {
    if(order.base == val){
        order.desc = !order.desc
    }else{
        order.base = val
    }
    LoadData()
}

const OrderData = () =>{
    data.sort((a, b) => {
        const prop = order.base;
        const desc = order.desc;
        
        const compareValues = (valueA, valueB) => {
            return desc ? valueA - valueB : valueB - valueA;
        };
    
        switch (prop) {
            case "nev":
                data.sort((a, b) => {
                    const fa = a.nev.toLowerCase();
                    const fb = b.nev.toLowerCase();
                
                    return order.desc ? fa.localeCompare(fb) : fb.localeCompare(fa);
                });
                
            case "id":
                return compareValues(a.omAz, b.omAz);
    
            case "matek":
                return compareValues(a.matek, b.matek);
    
            case "magyar":
                return compareValues(a.magyar, b.magyar);
    
            case "osszes":
                return compareValues(a.osszes, b.osszes);
    
            default:
                return 0;
        }
    });
    
}

const SetLimit = () => {
    let search = document.getElementById("filter-id").value;
    if(search > 100){
        document.getElementById("filter-id").value = 100
    }else if(search < 0){
        document.getElementById("filter-id").value = 0
    }
    
    limit = search
    LoadData()
}

const LoadData = () => {
    OrderData()
    let tb = document.getElementById("table-body")
    tb.innerHTML = ""
    data.forEach((row)=>{
        if(row.osszes >= limit){ 
            tb.appendChild(row.ConvertToRow())
        }
    })
}

const SearchUser = () => {
    const searchInput = document.getElementById("searchInput").value
    pgUserAdd.style.display = "none"
    pgNotFound.style.display = "none"
    pgUserFound.style.display = "none"
    if(searchInput.length === 11 && data.find(({omAz}) => omAz === searchInput)){
        let userData = data.find(({omAz}) => omAz === searchInput).ConvertToData()
        pgUserFound.style.display = "flex"
        pgUserFound.innerHTML = ""
        pgUserFound.appendChild(userData)
    }else{
        pgNotFound.style.display = "flex"
    }
}

const HandleInput = () => { 
    const options = document.getElementById("ids")
    options.innerHTML = ""
    let search = document.getElementById("searchInput").value;
    search = search.slice(0, 11);  
    
    let ids = []
    data.forEach((item)=>{
        if(search === item.omAz.slice(0,search.length)){
            ids.push(item.omAz)
        }
    })
    ids.forEach((id)=>{
        let option = document.createElement("option")
        option.innerText = id
        options.appendChild(option)
    })
}
