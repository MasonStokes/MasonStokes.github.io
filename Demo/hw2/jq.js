//document.getElementById("submit").addEventListener("click", submitButton(event));

function submitButton(){
    
    var numb = $("ssn").val();
    var numbList = [];
    numbList[0] = String(numb).charAt(0);
    numbList[1] =  String(numb).charAt(1);
    numbList[2] =  String(numb).charAt(2);

    var list = document.createElement('ol');
    //list.setAttribute('id', 'newOl');
    
    var returnList = ['new identities.', 'new credit cars', 'new houses.'];

    //document.getElementById('newOl').appendChild(ul);
    
    for(var i = 0; i < returnList.length; i++){
        var listItem = document.createElement('li');

        //Set list item contents
        listItem.appendChild(document.createTextNode('You now have' + numbList[i], returnList[i]));

        //fill list with items
        list.appendChild(listItem);
    }
    //console.log(list)
    
}

