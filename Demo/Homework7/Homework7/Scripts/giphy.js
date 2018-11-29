
var simpleWords = ["I", "the", "be", "to", "of", "and", "a", "in", "that", "have", "it", "we", "say", "him", "her", "or", "an", "will", "me", "again",
        "for", "not", "on", "with", "he", "she", "as", "you", "do", "at", "this", "one", "all", "would", "there", "their", "what", "when",
        "but", "his", "hers", "by", "from", "they", "so", "up", "out", "if", "about", "who", "get", "which", "go", "me", "public", "are",
        "make", "can", "like", "time", "no", "just", "know", "take", "people", "into", "year", "your", "good", "some", "could", "always", "went",
        "them", "see", "other", "than", "then", "now", "look", "only", "come", "its", "it's", "over", "think", "also", "back", "really", "store",
        "after", "use", "two", "how", "our", "work", "first", "well", "way", "even", "new", "want", "because", "any", "these", "give", "every",
        "day", "most", "us", "my", "mine", "yours", "they're", "I'm", "going", "took", "huge", "big", "large", "very", "am", "let's", "had",
        "try", "wicked", "pretty", "is", "please", "was", "name", "named", "stay", "away", "any", "anymore", "we're", "brought", "does", "did"];


/**
 * Helper function to get the length of the entire string in the input field.
 */
function searchInputLength() {
    console.log("searchInputLength");
    return $("#searchInput").val().length;
}

/**
 * Helper function to compare the word to our list of "fun" words.
 * @param {any} lastWord The last word typed into the input field.
 */
function isGifable(lastWord) {
    console.log("gifable start");
    var temp = lastWord;
    
    var isGifable = true;
    for (var i = 0; i < simpleWords.length; i++) {
        if (simpleWords[i].toLowerCase() == temp.toLowerCase()) {
            isGifable = false;
        }
    }
    console.log("gifable end");
    return isGifable;
}

/**
 * This function extracts the image URL from the JSON object and uses it
 * to insert the gif into the view.
 * @param {any} result The JSON object sent from the controller.
 */
function insertGif(result) {
    console.log("insert gif start");
    var giphyURL = result.data.images.preview_gif.url;
    
    console.log(giphyURL)
    
    $("#message").append("<img width='100px;' height='100px;' src='" + giphyURL + "'/>");
    console.log("insert gif end");
}

/**
 * This function inserts the word passed in directly to the view.
 * @param {any} lastWord The word to insert in the view.
 */
function insertWord(lastWord) {
    console.log("insert word start");
    $("#message").append(" " + "<label> " + lastWord + " </label>");
    console.log("insert word end");
}

/**
 * This function gets the last word typed into the input field.
 */
function getLastWord() {
    console.log("get last word start");
    var lastWord = "";
    
    var input = document.getElementById("searchInput").value;
    
    var parsedInput = input.substr(0, input.length).replace(/[.,\/#!$%\^&\*;:{}=\-_`~()]/g, "");
    
    if (/\S/.test(parsedInput)) {
        
        lastWord = parsedInput.split(" ").pop();
    }

    /* Return the word */
    console.log("get last word end");
    return lastWord;
}

function ajaxError() {
    console.log("error loading picture");
}

/**
 * This function determins what to do with the word we grabbed.
 * @param {any} lastWord The last word typed into the input field.
 */
function chooseAction(lastWord) {
    console.log("choose action start");
    if (isGifable(lastWord)) {
        console.log("insertGif");
        var src = "/Giphy/RandomGif/" + lastWord;
        console.log("src")
        $.ajax({

            type: "GET",
            dataType: "json",
            url: src,
            success: insertGif,
            error: ajaxError
        });
    }
    else {

        insertWord(lastWord);
    }
    console.log("choose action end");
}


function main() {
    console.log("start main");

    $("#searchInput").focus();
    console.log("focused");
    /* Callback function that listens for the spacebar to be pressed */
    $("#searchInput").bind('keypress', function (e) {
        if (e.which === 32) {
            console.log("is space");
            var lastWord = getLastWord();
            console.log("last word taken");
            chooseAction(lastWord);
            console.log("action chosen");
            $("#searchInput").focus();
        }
    });
}

main();
