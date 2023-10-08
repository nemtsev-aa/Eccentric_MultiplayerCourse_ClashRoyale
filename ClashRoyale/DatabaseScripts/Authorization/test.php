<?php
    require '../database.php';

    $user = R::load('users', 1);
    $card1 = R::load('cards', 1);
    $card2 = R::load('cards', 3);
    $card3 = R::load('cards', 5);

    $user -> link('cards_users', array('selected' => false)) -> cards = $card1;
    $user -> link('cards_users', array('selected' => false)) -> cards = $card2;
    $user -> link('cards_users', array('selected' => false)) -> cards = $card3;

    // $user -> sharedCardsList[] = $card1;     // создание связи
    // unset($user -> sharedCardsList[5]);      // удаление связи

    R::store($user);
?>