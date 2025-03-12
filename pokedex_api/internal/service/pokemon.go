package service

type PokemonService interface {
	GetPokemonById(id string) (*model.Pokemon, error)
}

type pokemonService struct {
}

func NewPokemonService() PokemonService {
	return &pokemonService{}
}

func (s *pokemonService) GetPokemonById(id string) (*model.Pokemon, error) {

}
