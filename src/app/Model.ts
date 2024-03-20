
export class Model{
domains:Array<Domain>=[];

constructor(){    
}
}

export class Domain{
    domainId: number=0;
    domainName:string="";
    avalibilityStatus:boolean=false;
    lastDateOfCheck:Date=new Date;
    dateOfExpire:Date=new Date;

    constructor(domainId:number,domainName:string,avalibilityStatus:boolean,lastDateOfCheck:Date,dateOfExpire:Date)
    {
        this.domainId=domainId;
        this.domainName=domainName;
        this.avalibilityStatus=avalibilityStatus;
        this.lastDateOfCheck=lastDateOfCheck;
        this.dateOfExpire=dateOfExpire;
    }
    
}