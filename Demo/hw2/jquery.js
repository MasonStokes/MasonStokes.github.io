document.getElementById("submit").addEventListener("click", submitButton(event));

function submitButton(){
    
    var numb = document.getElementById("ssn")[].value;
    var numbList = [];
    numbList[1] = String(numb).charAt(0);
    numbList[2] =  String(numb).charAt(1);
    numbList[3] =  String(numb).charAt(2);

    var ol = document.createElement('ol');
    ol.setAttribute('id', 'newOl');
    
    var returnList = ['You now have', 'new identities.', 'new credit cars', 'new houses.'];

    document.getElementById('newOl').appendChild(ul);
    
    for(var i = 1; i < 4; i++){
        var listItem = document.createElement('li');
        listItem.appendChild(document.createTextNode(returnList[0], numbList[i], returnList[i]));
        ol.appendChild(listItem);
    }
    
}

