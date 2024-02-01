const data = [
    new Diak("11234567890","Joe Biden",5,50),
    new Diak("32334567890","Kama Kálmán",25,40),
    new Diak("11238754890","Nem Joe Biden",12,24),
    new Diak("11234568760","Gipsz Jakab",12,32),
    new Diak("11276523440","Fartin' Martin",1,1),
    new Diak("98657574765","Lapát János",5,1),
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
    limit = document.getElementById("filter-id").value
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