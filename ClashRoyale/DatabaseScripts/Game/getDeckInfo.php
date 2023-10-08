<?php
    require '../database.php';

    $userID = $_POST['userID'];

    if(!isset($userID)){
        echo 'Data struct error!';
        exit;
    }

    $user = R::load('users', $userID);
    $allCards = $user -> sharedCards;
   
    $availableCards = [];
    foreach($allCards as $card){
        $availableCards[] = $card -> export();
    }
    $availableCardsJson = json_encode($availableCards);

    $selectedCardsBeans = $user -> withCondition('cards_users.selected =?', array(true)) -> sharedCards;
    $selectedIDsJson = json_encode(array_column($selectedCardsBeans, 'id'));

    echo '{"availableCards":' . $availableCardsJson . ', "selectedIDs":' . $selectedIDsJson .'}';
?>