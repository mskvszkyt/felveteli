class Diak{
    constructor(omAz,nev,email,szulDat,ertCim,matek,magyar){
        this.omAz = omAz;
        this.nev = nev;
        this.email = email;
        this.szulDat = szulDat;
        this.ertCim = ertCim;
        this.matek = matek;
        this.magyar = magyar;
        this.osszes = matek+magyar;
    }

    ConvertToRow = () => {
        let tr = document.createElement("tr")
        tr.ondblclick = () => {this.CopyData()}
        let data = [this.omAz,this.nev,this.matek,this.magyar,this.osszes]
        data.forEach((value)=>{
            let td = document.createElement("td")
            td.innerText = value
            tr.appendChild(td)
        })
        return tr
    }

    ConvertToData = () => {
        let data = document.createElement("div");
        data.classList.add("student-data"); 
        
        let head = document.createElement("div");
        head.classList.add("data-head");
        
        let name = document.createElement("h1");
        name.innerText = this.nev;
        
        head.appendChild(name);
        
        let id = document.createElement("h4");
        id.innerText = this.omAz;
        head.appendChild(id);
        
        data.appendChild(head);
        
        let scoreContainer = document.createElement("div");
        scoreContainer.classList.add("score-container");
        let scoreText = document.createElement("h3")        
        scoreText.innerText = `Matematika: ${this.matek} + Magyar: ${this.magyar}`

        scoreContainer.appendChild(scoreText); 
        
        data.appendChild(scoreContainer);
        
        let score = document.createElement("h2");
        score.innerText = `Összes pontszám: ${this.osszes}`;
        data.appendChild(score);
        
        return data
    }

    ConvertToCSV = () => {
        return `${this.omAz};${this.nev};${this.email};${this.szulDat};${this.ertCim};${this.matek};${this.magyar}`
    }

    CopyData = async () => {
        await navigator.clipboard.writeText(this.ConvertToCSV());

        alert(`${this.omAz} azonsítójú diák CSV formátumban vágólapra mentve!`);
    }
}

