const data = [
    new Diak("98657574765", "Gipsz János","janos@gmail.com","1997-06-14","Hódmezővásárhely", 50, 1),
    new Diak("12345678901", "Kovács Eszter","eszter@gmail.com", "1997-06-14","Hódmezővásárhely", 43, 3),
    new Diak("98765432109", "Nagy Gábor","gabor@gmail.com", "1997-06-14","Hódmezővásárhely", 2, 44),
    new Diak("55555555555", "Varga Anna","anna@gmail.com", "1997-06-14","Hódmezővásárhely", 13, 5),
    new Diak("77777777777", "Kis Péter","peter@gmail.com", "1997-06-14","Hódmezővásárhely", 3, 22),
]

const pgNotFound = document.getElementById("id-not-found-page")
const pgUserFound = document.getElementById("id-found-page")
const pgUserAdd = document.getElementById("create-student-page")

let order = {
    base: "nev",
    desc: false
}

let limit = 0

const SetOrder = (val) => {
    let element = document.getElementById(val) 
    if(order.base == val){
        order.desc = !order.desc 
        let replacedData = element.innerHTML.replace("▼","")
        replacedData = replacedData.replace("▲","")
        element.innerHTML = replacedData
        
    }else{
        let lastSelected = document.getElementById(order.base)
        let replacedData = lastSelected.innerHTML.replace(order.desc ? "▼" : "▲", "")
        lastSelected.innerHTML = replacedData
        order.base = val
        order.desc = true
    }
    element.innerHTML += order.desc ? "▼" : "▲"
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
    let searchInput = document.getElementById("searchInput");
    let search = searchInput.value;

    if (search.length > 11) {
        search = search.slice(0, 11);
        searchInput.value = search;
    }
    let ids = []
    let mathAvgVal = 0
    let litAvgVal = 0
    let sumAvgVal = 0
    data.forEach((item)=>{
        if(search === item.omAz.slice(0,search.length)){
            ids.push(item.omAz)
            mathAvgVal += item.matek
            litAvgVal += item.magyar
            sumAvgVal += item.osszes
        }
    })
    resCount.innerText = ids.length
    mathAvg.innerText = ids.length > 0 ? (mathAvgVal / ids.length).toFixed(1) : 0
    litAvg.innerText = ids.length > 0 ? (litAvgVal / ids.length).toFixed(1) : 0
    sumAvg.innerText = ids.length > 0 ? (sumAvgVal / ids.length).toFixed(1) : 0
    ids.forEach((id)=>{
        let option = document.createElement("option")
        option.innerText = id
        options.appendChild(option)
    })
}

const ExportData = async () => {
    csvData = "OM_azonosito;Nev;Email;Szuletesi_datum;Ertesitesi_cim;matek_eredmeny;magyar_eredmeny"
    data.forEach((line)=>{
        csvData += `${line.ConvertToCSV()}\n`
    })

    var hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csvData);
    hiddenElement.target = '_blank';  
    hiddenElement.download = 'Diakok.csv';  
    hiddenElement.click();   
}
HandleInput()
SetOrder("nev")