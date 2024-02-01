class Diak{
    constructor(omAz,nev,matek,magyar){
        this.omAz = omAz;
        this.nev = nev;
        this.matek = matek;
        this.magyar = magyar;
        this.osszes = matek+magyar;
    }

    ConvertToRow = () => {
        let tr = document.createElement("tr")
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
        
        let btn = document.createElement("button");
        btn.id="edit-button"
        btn.innerText = "✎";
        data.appendChild(btn);
        
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
        
        let mathScore = document.createElement("h3")
        mathScore.innerText = this.matek;
        let plusSign = document.createElement("h3")
        plusSign.innerText = "+";
        let litScore = document.createElement("h3")
        litScore.innerText = this.magyar;
        
        scoreContainer.appendChild(mathScore);
        scoreContainer.appendChild(plusSign);
        scoreContainer.appendChild(litScore);
        
        data.appendChild(scoreContainer);
        
        let score = document.createElement("h2");
        score.innerText = this.osszes;
        data.appendChild(score);
        
        return data
    }
}