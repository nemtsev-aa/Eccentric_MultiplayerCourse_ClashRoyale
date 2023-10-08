<?php
    require '../database.php';
    $cardsNames = array('Archer_1', 'Archer_2', 'Golem_1', 'Golem_2', 'Warrior_1', 'Warrior_2');

    foreach($cardsNames as $name){
        $card = R::dispense('cards');
        $card -> name = $name;
        R::store($card);
    }
?>