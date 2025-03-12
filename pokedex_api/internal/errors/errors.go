package errors

import "errors"

var ErrPokemonApi = errors.New("pokemon api error")
var ErrPokemonNotFound = errors.New("pokemon not found")
