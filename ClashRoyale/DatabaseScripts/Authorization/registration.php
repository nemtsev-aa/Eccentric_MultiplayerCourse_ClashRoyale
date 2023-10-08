<?php
    require '../System/configConstants.php';
    require '../database.php';

    $login = $_POST['login'];
    $password = $_POST['password'];

    if(!isset($login) || !isset($password)){
        echo 'Data struct error!';
        exit;
    }

    $repeatCheker = R::findOne('users', 'login = ?', array($login));
    if(isset($repeatCheker)){
        echo 'Login reserved!';
        exit;
    }

    $user = R::dispense('users');
    $user -> login = $login;
    $user -> password = $password;

    R::store($user);
    echo 'ok';

    $availableCards = DEFAULT_AVAILABLE_CARDS;
    foreach($availableCards as $id){
        $card = R::load('cards', $id);
        $user -> link('cards_users', array('selected' => false)) -> cards = $card;
    }

    R::store($user);
    

    $selectedID = DEFAULT_SELECTED_CARDS;
    $links = $user -> withCondition('cards_users.cards_id IN ('. R::genSlots($selectedID) .')', $selectedID) -> ownCardsUsers;
    foreach($links as $link){
        $link -> selected = true; 
    }

    R::store($user);
?>